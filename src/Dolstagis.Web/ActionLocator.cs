using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dolstagis.Web.Handlers;
using Dolstagis.Web.Http;
using Dolstagis.Web.Routing;

namespace Dolstagis.Web
{
    public class ActionLocator : IActionLocator
    {
        private HandlerRegistry handlers;
        private Func<IAction> getAction;

        public ActionLocator(HandlerRegistry handlers, Func<IAction> getAction)
        {
            this.handlers = handlers;
            this.getAction = getAction;
        }

        public IAction GetAction(IRequest request)
        {
            var registry = this.handlers.GetHandlerRegistration(request.AppRelativePath, false);
            if (registry == null || registry.Item1 == null || !registry.Item1.IsValid) return null;

            var type = registry.Item1.Definition.Type;
            var pathInfo = registry.Item2;

            var method = type.GetMethod
                (request.Method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (method == null) return null;

            var attr = method.GetCustomAttribute<ParametersAttribute>();
            if (attr == null && !String.IsNullOrEmpty(pathInfo)) return null;

            var args = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (attr != null) {
                var keys = attr.Components;
                var values = pathInfo.Split(new char[] { '/' }, keys.Count);
                for (var i = 0; i < values.Length; i++) {
                    args[keys[i]] = values[i];
                }
            }

            var parameters = new List<object>();

            foreach (var param in method.GetParameters()) {
                string value;
                if (args.TryGetValue(param.Name, out value)) {
                    parameters.Add(value);
                }
                else if (param.IsOptional) {
                    parameters.Add(param.DefaultValue);
                }
                else {
                    return null;
                }
            }

            // TODO: add GET, POST values
            // TODO: add converters to int, boolean etc
            // TODO: add model binders

            var action = getAction();
            action.HandlerType = type;
            action.Method = method;
            action.Parameters = parameters.ToArray();
            return action;
        }
    }
}

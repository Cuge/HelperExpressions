using EnvDTE;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4HelperMethods
{
    public class EnvHelperMethods
    {
        EnvDTE.DTE _DTE;

        public EnvHelperMethods(ITextTemplatingEngineHost host)
        {
            _DTE = GetEnvDTE(host);
        }

        public List<EnvDTE.ProjectItem> GetProjectItemsRecursively(EnvDTE.ProjectItems items)
        {
            var ret = new List<EnvDTE.ProjectItem>();
            if (items == null) return ret;
            foreach (EnvDTE.ProjectItem item in items)
            {
                ret.Add(item);
                ret.AddRange(GetProjectItemsRecursively(item.ProjectItems));
            }
            return ret;
        }

        public IEnumerable<CodeClass> GetCodeClasses(EnvDTE.ProjectItems items)
        {
            IEnumerable<ProjectItem> projectItems = GetProjectItemsRecursively(items);
            List<CodeClass> ret = new List<CodeClass>();
            foreach (ProjectItem projectItem in projectItems)
            {
                ret.AddRange(GetCodeClasses(projectItem));
            }
            return ret;
        }
        public IEnumerable<CodeClass> GetCodeClasses(ProjectItem item)
        {
            List<CodeClass> ret = new List<CodeClass>();
            if(item.FileCodeModel != null)
            {
                foreach (EnvDTE.CodeElement codeElement in item.FileCodeModel.CodeElements)
                {
                    CodeClass codeClass = codeElement as CodeClass;
                    if (codeClass != null)
                        ret.Add(codeClass);
                }
            }
            return ret;
        }

        public CodeNamespace FindNamespace(CodeElements elements)
        {
            foreach (CodeElement element in elements)
            {
                CodeNamespace ns = element as CodeNamespace;

                if (ns != null)
                    return ns;
            }

            return null;
        }
        public CodeInterface FindInterface(CodeElements elements)
        {
            foreach (CodeElement element in elements)
            {
                CodeInterface codeInterface = element as CodeInterface;
                if (codeInterface != null)
                    return codeInterface;
                codeInterface = FindInterface(element.Children);
                if (codeInterface != null)
                    return codeInterface;
            }
            return null;
        }

        public List<CodeFunction> FindMethods(CodeElements elements)
        {
            List<CodeFunction> methods = new List<CodeFunction>();

            foreach (CodeElement element in elements)
            {
                CodeFunction method = element as CodeFunction;

                if (method != null)
                    methods.Add(method);
            }

            return methods;
        }

        public EnvDTE.DTE GetEnvDTE(ITextTemplatingEngineHost host)
        {
            IServiceProvider hostServiceProvider = (IServiceProvider)host;
            if (hostServiceProvider == null)
                throw new Exception("Host property returned unexpected value (null)");
            EnvDTE.DTE dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));
            if (dte == null)
                throw new Exception("Unable to retrieve EnvDTE.DTE");
            return dte;
        }
    }
}

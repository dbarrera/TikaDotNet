using ikvm.runtime;
using java.lang;

namespace TikaDotNet.TextExtraction
{
    public class MySystemClassLoader : ClassLoader
    {
        public MySystemClassLoader(ClassLoader parent)
            : base(new AppDomainAssemblyClassLoader(typeof(MySystemClassLoader).Assembly))
        {
        }
    }
}

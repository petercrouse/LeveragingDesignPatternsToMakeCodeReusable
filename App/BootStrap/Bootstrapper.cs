using StructureMap;

namespace App.BootStrap
{
    public static class Bootstrapper
    {
        public static Container GetContainer()
        {
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(Program));
                    scanner.WithDefaultConventions();
                });
            });
            return container;
        }
    }
}

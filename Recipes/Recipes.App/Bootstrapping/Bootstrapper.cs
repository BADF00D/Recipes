using Castle.Windsor;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Windsor;


namespace Recipes.App.Bootstrapping
{
    //public class Bootstrapper : WindsorNancyBootstrapper
    //{
    //    public IWindsorContainer Container { get; }

    //    public Bootstrapper()
    //    {
    //        Container = new WindsorContainer();
    //    }

    //    protected override void ApplicationStartup(IWindsorContainer container, IPipelines pipelines)
    //    {
    //        base.ApplicationStartup(container, pipelines);
    //    }

    //    protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
    //    {
    //        //add installers here
    //    }

    //    protected override IWindsorContainer GetApplicationContainer()
    //    {
    //        return Container;
    //    }
    //}
}

using Owin;

namespace apptesisService
{
    public interface IStartup
    {
        void Configuration(IAppBuilder app);
    }
}
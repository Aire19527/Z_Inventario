using Domain.Services;
using Domain.Services.Interfaces;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interfaces;

namespace Web.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            //Repository await UnitofWork parameter ctor explicit
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));



            //Domain
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IInvoiceServices, InvoiceServices>();


        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using apptesisService.DataObjects;
using apptesisService.Models;

namespace apptesisService.Controllers
{
    public class clsCooperativaClienteController : TableController<clsCooperativaCliente>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CooperativaClienteServiceContext context = new CooperativaClienteServiceContext();
            DomainManager = new EntityDomainManager<clsCooperativaCliente>(context, Request);
        }

        // GET tables/clsCooperativaCliente
        public IQueryable<clsCooperativaCliente> GetAllclsCooperativaCliente()
        {
            return Query(); 
        }

        // GET tables/clsCooperativaCliente/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsCooperativaCliente> GetclsCooperativaCliente(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsCooperativaCliente/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsCooperativaCliente> PatchclsCooperativaCliente(string id, Delta<clsCooperativaCliente> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsCooperativaCliente
        public async Task<IHttpActionResult> PostclsCooperativaCliente(clsCooperativaCliente item)
        {
            clsCooperativaCliente current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsCooperativaCliente/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsCooperativaCliente(string id)
        {
             return DeleteAsync(id);
        }
    }
}

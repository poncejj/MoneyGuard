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
    public class clsTransferenciaController : TableController<clsTransferencia>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TransferenciaServiceContext context = new TransferenciaServiceContext();
            DomainManager = new EntityDomainManager<clsTransferencia>(context, Request);
        }

        // GET tables/clsTransferencia
        public IQueryable<clsTransferencia> GetAllclsTransferencia()
        {
            return Query(); 
        }

        // GET tables/clsTransferencia/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsTransferencia> GetclsTransferencia(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsTransferencia/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsTransferencia> PatchclsTransferencia(string id, Delta<clsTransferencia> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsTransferencia
        public async Task<IHttpActionResult> PostclsTransferencia(clsTransferencia item)
        {
            clsTransferencia current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsTransferencia/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsTransferencia(string id)
        {
             return DeleteAsync(id);
        }
    }
}

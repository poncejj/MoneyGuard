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
    public class clsCatalogoController : TableController<clsCatalogo>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            CatalogoServiceContext context = new CatalogoServiceContext();
            DomainManager = new EntityDomainManager<clsCatalogo>(context, Request);
        }

        // GET tables/clsCatalogo
        public IQueryable<clsCatalogo> GetAllclsCatalogo()
        {
            return Query(); 
        }

        // GET tables/clsCatalogo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsCatalogo> GetclsCatalogo(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsCatalogo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsCatalogo> PatchclsCatalogo(string id, Delta<clsCatalogo> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsCatalogo
        public async Task<IHttpActionResult> PostclsCatalogo(clsCatalogo item)
        {
            clsCatalogo current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsCatalogo/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsCatalogo(string id)
        {
             return DeleteAsync(id);
        }
    }
}

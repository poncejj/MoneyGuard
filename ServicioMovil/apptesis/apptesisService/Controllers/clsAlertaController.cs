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
    public class clsAlertaController : TableController<clsAlerta>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            AlertaServiceContext context = new AlertaServiceContext();
            DomainManager = new EntityDomainManager<clsAlerta>(context, Request);
        }

        // GET tables/clsAlerta
        public IQueryable<clsAlerta> GetAllclsAlerta()
        {
            return Query(); 
        }

        // GET tables/clsAlerta/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsAlerta> GetclsAlerta(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsAlerta/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsAlerta> PatchclsAlerta(string id, Delta<clsAlerta> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsAlerta
        public async Task<IHttpActionResult> PostclsAlerta(clsAlerta item)
        {
            clsAlerta current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsAlerta/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsAlerta(string id)
        {
             return DeleteAsync(id);
        }
    }
}

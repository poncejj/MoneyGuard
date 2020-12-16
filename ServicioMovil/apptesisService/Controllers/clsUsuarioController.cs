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
    public class clsUsuarioController : TableController<clsUsuario>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UsuarioServiceContext context = new UsuarioServiceContext();
            DomainManager = new EntityDomainManager<clsUsuario>(context, Request);
        }

        // GET tables/clsUsuario
        public IQueryable<clsUsuario> GetAllclsUsuario()
        {
            return Query(); 
        }

        // GET tables/clsUsuario/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<clsUsuario> GetclsUsuario(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/clsUsuario/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<clsUsuario> PatchclsUsuario(string id, Delta<clsUsuario> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/clsUsuario
        public async Task<IHttpActionResult> PostclsUsuario(clsUsuario item)
        {
            clsUsuario current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/clsUsuario/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteclsUsuario(string id)
        {
             return DeleteAsync(id);
        }
    }
}

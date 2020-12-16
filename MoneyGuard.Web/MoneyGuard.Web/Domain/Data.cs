using MoneyGuard.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            menu.Add(new Navbar { Id = 1, nameOption = "Inicio", controller = "Home", action = "Index", imageClass = "fa fa-home fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Applicación", imageClass = "fa fa-mobile fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Usuarios", controller = "Aplicacion", action = "UsuarioApp", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 4, nameOption = "Cuentas Usuarios", controller = "Aplicacion", action = "CooperativaUsuarioApp", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 5, nameOption = "Cooperativas Registradas", controller = "Aplicacion", action = "Cooperativa", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 6, nameOption = "Catalogos", controller = "Aplicacion", action = "Catalogo", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 7, nameOption = "Transacciones", controller = "Aplicacion", action = "Transacciones", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 8, nameOption = "Alertas", controller = "Aplicacion", action = "Alertas", status = true, isParent = false, parentId = 2 });
            menu.Add(new Navbar { Id = 9, nameOption = "Cooperativa Amarilla", imageClass = "fa fa-bank fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 10, nameOption = "Clientes", controller = "Cooperativa", action = "Clientes", status = true, isParent = false, parentId = 9, cooperativa = "CooperativaAmarilla" });
            menu.Add(new Navbar { Id = 11, nameOption = "Cuentas", controller = "Cooperativa", action = "Cuentas", status = true, isParent = false, parentId = 9, cooperativa = "CooperativaAmarilla" });
            menu.Add(new Navbar { Id = 12, nameOption = "Tarjetas de Credito", controller = "Cooperativa", action = "TarjetasCredito", status = false, isParent = false, parentId = 9, cooperativa = "CooperativaAmarilla" });
            menu.Add(new Navbar { Id = 13, nameOption = "Movimientos", controller = "Cooperativa", action = "Movimientos", status = true, isParent = false, parentId = 9, cooperativa = "CooperativaAmarilla" });
            menu.Add(new Navbar { Id = 14, nameOption = "Cooperativa Verde", imageClass = "fa fa-bank fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 15, nameOption = "Clientes", controller = "Cooperativa", action = "Clientes", status = true, isParent = false, parentId = 14, cooperativa = "CooperativaVerde" });
            menu.Add(new Navbar { Id = 16, nameOption = "Cuentas", controller = "Cooperativa", action = "Cuentas", status = true, isParent = false, parentId = 14, cooperativa = "CooperativaVerde" });
            menu.Add(new Navbar { Id = 17, nameOption = "Tarjetas de Credito", controller = "Cooperativa", action = "TarjetasCredito", status = true, isParent = false, parentId = 14, cooperativa = "CooperativaVerde" });
            menu.Add(new Navbar { Id = 18, nameOption = "Movimientos", controller = "Cooperativa", action = "Movimientos", status = true, isParent = false, parentId = 14, cooperativa= "CooperativaVerde" });
            menu.Add(new Navbar { Id = 19, nameOption = "Servicios Basicos", imageClass = "fa fa-lightbulb-o fa-fw", status = true, isParent = true, parentId = 0 });
            menu.Add(new Navbar { Id = 20, nameOption = "Clientes", controller = "ServiciosBasicos", action = "Cliente", status = true, isParent = false, parentId = 19});
            menu.Add(new Navbar { Id = 21, nameOption = "Suministros", controller = "ServiciosBasicos", action = "Suministro", status = true, isParent = false, parentId = 19});
            menu.Add(new Navbar { Id = 22, nameOption = "Facturas", controller = "ServiciosBasicos", action = "Factura", status = true, isParent = false, parentId = 19});

            return menu.ToList();
        }
    }
}
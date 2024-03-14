using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreSeguridadPersonalizada.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //ESTE METODO IMPEDIRA ACCEDER A LAS ZONAS QUE HAYAMOS
        //DECORADO

        //DICHO FILTER DEBE VALIDAR SI EXISTIMOS EN LA APP O NO.
        //SI NO ESTAMOS VALIDADOS EN NUESTRA APP, NOS LLEVARA A LOGIN
        //SI ESTAMOS VALIDADOS, NO HACEMOS NADA

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //EL USUARIO ESTARA DENTRO DE HttpContext
            //Y SU PROPIEDAD User
            //ESE USUARIO PERTENECE A LA CLASE PRINCIPAL E IDENTITY
            //MEDIANTE LA IDENTIDAD, PODEMOS SABER LE NOMBRE DEL USER
            //MEDIANTE EL PRINCIPAL PODEMOS SABER EL ROLE DEL USER

            var user = context.HttpContext.User;
            //PREGUNTAMOS SI EL USER YA ESTA AUTENTIFICADO
            if (user.Identity.IsAuthenticated == false)
            {
                //CREAMOS LA RUTA A NUESTRA DIRECCION
                RouteValueDictionary rutaLogin =
                    new RouteValueDictionary
                    (
                        new { controller = "Managed", action = "Login" }
                    );
                //LLEVAMOS AL USUARIO A Login
                context.Result =
                    new RedirectToRouteResult(rutaLogin);
            }

        }
    }
}

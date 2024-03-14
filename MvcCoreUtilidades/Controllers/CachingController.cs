using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class CachingController : Controller
{
    private IMemoryCache memoryCache;

    public CachingController(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }
    //PODEMOS INDICAR EL TIEMPO EN SEGUNDS PARA QUE
    //RESPONDA DE NUEVO AL ACTION
    [ResponseCache(Duration = 15,
        Location =ResponseCacheLocation.Client)]
    public IActionResult MemoriaDistribuida()
    {
        string fecha = DateTime.Now.ToLongDateString() + " -- "
            + DateTime.Now.ToLongTimeString();
        ViewData["FECHA"] = fecha;
        return View();
    }

    public IActionResult MemoriaPersonalizada(int? tiempo)
    {
        if (tiempo == null)
        {
            tiempo = 25;
        }
        string fecha = DateTime.Now.ToLongDateString()
            + " -- " + DateTime.Now.ToLongTimeString();
        // Preguntamos si existe algo en cache o no
        if (this.memoryCache.Get("FECHA") == null)
        {
            // Creamos las opciones para el caché con tiempo
            MemoryCacheEntryOptions options =
                new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(tiempo.Value));
            this.memoryCache.Set("FECHA", fecha, options);
            ViewData["MENSAJE"] = "Almacenando en cache";
            ViewData["FECHA"] = fecha;
        }
        else
        {
            fecha = this.memoryCache.Get<string>("FECHA");
            ViewData["MENSAJE"] = "Recuperando de cache";
            ViewData["FECHA"] = fecha;
        }
        return View();
    }
}
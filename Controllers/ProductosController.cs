using Microsoft.AspNetCore.Mvc;
using WebSocket.Clases;
using WebSocket.Models;

namespace WebSocket.Controllers
{
    public class ProductosController : Controller
    {
        public async Task<ActionResult>  ListaProductos()
        {
            return await Task.Run(() => View());
        }

        public List<ProductoCLS> ListarProductos(string descripcion)
        {
            List<ProductoCLS> lista = new List<ProductoCLS>();

            try
            {               
                using(db_a91cf7_dbreportesContext db = new db_a91cf7_dbreportesContext())
                {
                    if (descripcion == null || descripcion == "")
                    {
                        lista = (from producto in db.Productos
                                 where producto.Bhabilitado == 1
                                 select new ProductoCLS
                                 {
                                     iidproducto = producto.Iidproducto,
                                     description = producto.Descripcion,
                                     preciocadena = "$/"+ ((decimal)producto.Precio).ToString(),
                                     stockcadena = ((int)producto.Stock).ToString() + " unid"

                                 }).ToList();
                    }
                    else
                    {
                        lista = (from producto in db.Productos
                                 where producto.Bhabilitado == 1
                                 && producto.Descripcion.Contains(descripcion)
                                 select new ProductoCLS
                                 {
                                     iidproducto = producto.Iidproducto,
                                     description = producto.Descripcion,
                                     preciocadena = "$/" + ((decimal)producto.Precio).ToString(),
                                     stockcadena = ((int)producto.Stock).ToString() + " unid"

                                 }).ToList();
                    }

                    return lista;
                }
            }
            catch (Exception)
            {
                return lista;
            }            
        }

        public int GuardarProducto(ProductoCLS productoParam, IFormFile fotoParam)
        {
            int resp = 0;

            try
            {
                int idProducto = productoParam.iidproducto;
                byte[] foto = new byte[0];
                string nombreFoto = "";

                if(fotoParam != null)
                {
                    using(MemoryStream ms = new MemoryStream())
                    {
                        fotoParam.CopyTo(ms);
                        nombreFoto = fotoParam.FileName;
                        foto = ms.ToArray();
                    }
                }

                using (db_a91cf7_dbreportesContext db = new db_a91cf7_dbreportesContext())
                {
                    if (idProducto == 0)
                    {
                        var producto = new Producto()
                        {
                            Descripcion = productoParam.description,
                            Precio = productoParam.precio,
                            Stock = productoParam.stock,
                            Bhabilitado = 1                            
                        };

                        if(producto.Nombrefoto != "")
                        {
                            producto.Foto = foto;
                            producto.Nombrefoto = nombreFoto;
                        }

                        db.Productos.Add(producto);
                        db.SaveChanges();

                        resp = 1;
                    }
                    else
                    {
                        var producto = db.Productos.Where(x => x.Iidproducto == idProducto).First();

                        producto.Descripcion = productoParam.description;
                        producto.Precio = productoParam.precio;
                        producto.Stock = productoParam.stock;

                        if(nombreFoto != "")
                        {
                            producto.Foto = foto;
                            producto.Nombrefoto = nombreFoto;
                        }

                        //db.Productos.Update(producto);
                        db.SaveChanges();

                        resp = 1;
                    }
                }                
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }

            return resp;
        }

        public int EliminarProducto(int id)
        {
            int resp = 0;

            try
            {
                using (db_a91cf7_dbreportesContext db = new db_a91cf7_dbreportesContext())
                {
                    var producto = db.Productos.Where(x => x.Iidproducto == id).First();

                    producto.Bhabilitado = 0;

                    db.SaveChanges();
                }

                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }

            return resp;
        }

        public ProductoCLS RecuperarProducto(int id)
        {
            ProductoCLS productoCLS;

            try
            {
                using (db_a91cf7_dbreportesContext db = new db_a91cf7_dbreportesContext())
                {
                    productoCLS = (from producto in db.Productos
                                   where producto.Iidproducto == id
                                   select new ProductoCLS
                                   {
                                       iidproducto = producto.Iidproducto,
                                       description = producto.Descripcion,
                                       precio = (decimal)producto.Precio,
                                       stock = (int)producto.Stock,
                                       foto = producto.Foto == null ? "" :
                                          "data:image/" + Path.GetExtension(producto.Nombrefoto).Replace(".", "") + ";base64," 
                                          + Convert.ToBase64String(producto.Foto)

                                   }).First();
                }
            }
            catch (Exception)
            {
                productoCLS = new ProductoCLS();
                throw;
            }

            return productoCLS;
        }
    }
}

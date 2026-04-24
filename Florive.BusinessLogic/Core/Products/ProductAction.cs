using Florive.Domains.Entities;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using Florive.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Florive.BusinessLogic.Core.Products
{
    public class ProductAction
    {
        private readonly AppDbContext _context;

        public ProductAction(AppDbContext context)
        {
            _context = context;
        }

        protected ResponseMsg ExecuteGetAllProductsAction()
        {
            try
            {
                var products = _context.Products.ToList();

                var result = new List<ProductDTO>();
                foreach (var product in products)
                {
                    result.Add(new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Category = product.Category,
                        Image = product.Image
                    });
                }

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Успешно получены продукты",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении продуктов: {ex.Message}"
                };
            }
        }

        protected ResponseMsg GetProductDataByIdAction(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID должен быть больше 0"
                    };
                }

                var product = _context.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                var productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Image = product.Image
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт найден",
                    Data = productDTO
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при получении продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductCreateAction(ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные продукта не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Название продукта обязательно"
                    };
                }

                if (product.Price <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Цена должна быть больше 0"
                    };
                }

                var newProduct = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Image = product.Image
                };

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт создан успешно",
                    Data = newProduct
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при создании продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductUpdateAction(int id, ProductDTO product)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID должен быть больше 0"
                    };
                }

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Данные продукта не переданы"
                    };
                }

                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Название продукта обязательно"
                    };
                }

                if (product.Price <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Цена должна быть больше 0"
                    };
                }

                var existingProduct = _context.Products.FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Category = product.Category;
                existingProduct.Image = product.Image;

                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт обновлен успешно",
                    Data = existingProduct
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при обновлении продукта: {ex.Message}"
                };
            }
        }

        protected ResponseMsg ExecuteProductDeleteAction(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "ID должен быть больше 0"
                    };
                }

                var product = _context.Products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                _context.Products.Remove(product);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт удален успешно"
                };
            }
            catch (Exception ex)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = $"Ошибка при удалении продукта: {ex.Message}"
                };
            }
        }
    }
}

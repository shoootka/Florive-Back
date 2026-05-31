using Florive.DataAccess;
using Florive.Domains.Entities.Products;
using Florive.Domains.Models;
using Florive.Domains.Models.Base;
using Microsoft.EntityFrameworkCore;

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
                var products = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Description)
                        .ThenInclude(d => d.DescriptionAdvanced)
                    .ToList();

                var result = products.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Category = product.Category?.Name,
                    Image = product.Images.FirstOrDefault(i => i.IsMain)?.Url
                            ?? product.Images.FirstOrDefault()?.Url,
                    Images = product.Images.Select(i => i.Url).ToList(),
                    ShortDescription = product.Description?.ShortDescription,
                    FullDescription = product.Description?.FullDescription,
                    Width = product.Description?.DescriptionAdvanced?.Width,
                    Height = product.Description?.DescriptionAdvanced?.Height
                }).ToList();

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

                var product = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Description)
                        .ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == id);

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
                    CategoryId = product.CategoryId,
                    Category = product.Category?.Name,
                    Image = product.Images.FirstOrDefault(i => i.IsMain)?.Url
                            ?? product.Images.FirstOrDefault()?.Url,
                    Images = product.Images.Select(i => i.Url).ToList(),
                    ShortDescription = product.Description?.ShortDescription,
                    FullDescription = product.Description?.FullDescription,
                    Width = product.Description?.DescriptionAdvanced?.Width,
                    Height = product.Description?.DescriptionAdvanced?.Height
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

                CategoryData? category = null;

                if (product.CategoryId > 0)
                {
                    category = _context.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
                }

                if (category == null && !string.IsNullOrWhiteSpace(product.Category))
                {
                    category = _context.Categories
                        .FirstOrDefault(c => c.Name == product.Category);

                    if (category == null)
                    {
                        category = new CategoryData
                        {
                            Name = product.Category
                        };

                        _context.Categories.Add(category);
                        _context.SaveChanges();
                    }
                }

                if (category == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Категория обязательна"
                    };
                }

                var newProduct = new ProductData
                {
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = category.Id
                };

                if (!string.IsNullOrWhiteSpace(product.Image))
                {
                    newProduct.Images.Add(new ProductImgData
                    {
                        Url = product.Image,
                        IsMain = true
                    });
                }

                if (!string.IsNullOrWhiteSpace(product.ShortDescription) ||
                    !string.IsNullOrWhiteSpace(product.FullDescription) ||
                    product.Width.HasValue ||
                    product.Height.HasValue)
                {
                    newProduct.Description = new ProductDescriptionData
                    {
                        ShortDescription = product.ShortDescription ?? "",
                        FullDescription = product.FullDescription ?? "",
                        DescriptionAdvanced = new DescriptionAdvanced
                        {
                            Width = product.Width,
                            Height = product.Height
                        }
                    };
                }

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт создан успешно",
                    Data = new ProductDTO
                    {
                        Id = newProduct.Id,
                        Name = newProduct.Name,
                        Price = newProduct.Price,
                        CategoryId = newProduct.CategoryId,
                        Category = category.Name,
                        Image = newProduct.Images.FirstOrDefault(i => i.IsMain)?.Url
                                 ?? newProduct.Images.FirstOrDefault()?.Url,
                        Images = newProduct.Images.Select(i => i.Url).ToList(),
                        ShortDescription = newProduct.Description?.ShortDescription,
                        FullDescription = newProduct.Description?.FullDescription,
                        Width = newProduct.Description?.DescriptionAdvanced?.Width,
                        Height = newProduct.Description?.DescriptionAdvanced?.Height
                    }
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

                var existingProduct = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .Include(p => p.Description)
                        .ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == id);

                if (existingProduct == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = $"Продукт с ID {id} не найден"
                    };
                }

                existingProduct.Name = product.Name ?? existingProduct.Name;
                existingProduct.Price = product.Price;

                if (product.CategoryId > 0)
                {
                    existingProduct.CategoryId = product.CategoryId;
                }
                else if (!string.IsNullOrWhiteSpace(product.Category))
                {
                    var category = _context.Categories
                        .FirstOrDefault(c => c.Name == product.Category);

                    if (category == null)
                    {
                        category = new CategoryData
                        {
                            Name = product.Category
                        };

                        _context.Categories.Add(category);
                        _context.SaveChanges();
                    }

                    existingProduct.CategoryId = category.Id;
                }

                if (!string.IsNullOrWhiteSpace(product.Image))
                {
                    var mainImage = existingProduct.Images.FirstOrDefault(i => i.IsMain)
                                    ?? existingProduct.Images.FirstOrDefault();

                    if (mainImage == null)
                    {
                        existingProduct.Images.Add(new ProductImgData
                        {
                            Url = product.Image,
                            IsMain = true
                        });
                    }
                    else
                    {
                        mainImage.Url = product.Image;
                        mainImage.IsMain = true;
                    }
                }

                if (!string.IsNullOrWhiteSpace(product.ShortDescription) ||
                    !string.IsNullOrWhiteSpace(product.FullDescription) ||
                    product.Width.HasValue ||
                    product.Height.HasValue)
                {
                    if (existingProduct.Description == null)
                    {
                        existingProduct.Description = new ProductDescriptionData();
                    }

                    existingProduct.Description.ShortDescription = product.ShortDescription ?? "";
                    existingProduct.Description.FullDescription = product.FullDescription ?? "";

                    if (existingProduct.Description.DescriptionAdvanced == null)
                    {
                        existingProduct.Description.DescriptionAdvanced = new DescriptionAdvanced();
                    }

                    existingProduct.Description.DescriptionAdvanced.Width = product.Width ?? 0;
                    existingProduct.Description.DescriptionAdvanced.Height = product.Height ?? 0;
                }

                _context.SaveChanges();

                var productDTO = new ProductDTO
                {
                    Id = existingProduct.Id,
                    Name = existingProduct.Name,
                    Price = existingProduct.Price,
                    CategoryId = existingProduct.CategoryId,
                    Category = existingProduct.Category?.Name,
                    Image = existingProduct.Images.FirstOrDefault(i => i.IsMain)?.Url
            ?? existingProduct.Images.FirstOrDefault()?.Url,
                    Images = existingProduct.Images.Select(i => i.Url).ToList(),
                    ShortDescription = existingProduct.Description?.ShortDescription,
                    FullDescription = existingProduct.Description?.FullDescription,
                    Width = existingProduct.Description?.DescriptionAdvanced?.Width,
                    Height = existingProduct.Description?.DescriptionAdvanced?.Height
                };

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Продукт обновлен успешно",
                    Data = productDTO
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

                var product = _context.Products
                    .Include(p => p.Images)
                    .Include(p => p.Description)
                        .ThenInclude(d => d.DescriptionAdvanced)
                    .FirstOrDefault(p => p.Id == id);

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
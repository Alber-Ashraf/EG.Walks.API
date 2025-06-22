using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EG.Walks.Domain.Entities;

namespace EG.Walks.Application.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> ImageUploadAsync(Image image);
    }
}

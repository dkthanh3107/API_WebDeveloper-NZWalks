using MyNZWalks.API.Models.Domain;

namespace MyNZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

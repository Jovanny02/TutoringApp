using System.IO;
using System.Threading.Tasks;


namespace TutoringApp.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}

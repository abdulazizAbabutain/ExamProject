using Application.Commons.Services;

namespace Application.Commons.Managers
{
    public interface ISystemManager
    {
        ISystemService SystemService { get; }
    }
}

using WebApplication1.Models;

namespace WebApplication1.service.@abstract
{
    public interface IPostService
    {
        void savePost(PostSaveModel model);
        List<PostResponseModel> getPosts();
    }
}

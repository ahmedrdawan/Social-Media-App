

public interface IPostServices
{
    Task<ResponseModel> CreatePostAsync(Post post);
    Task<UserPostResponse?> GetPostByIdAsync(Guid id);
    Task<IEnumerable<UserPostResponse?>> GetAllPostsAsync();
    Task<ResponseModel> UpdatePostAsync(Guid id , Post post);
    Task<ResponseModel> DeletePostAsync(Guid id);
}


// after Finish Don't forget to implement the following features:
// ✔ Image upload (Cloudinary recommended)

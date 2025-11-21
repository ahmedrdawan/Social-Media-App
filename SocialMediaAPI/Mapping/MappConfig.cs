

using AutoMapper;

public class MappConfig : Profile
{
   public MappConfig()
   {
        CreateMap<RegisterRequest, User>();
        CreateMap<LoginRequest, User>();

        CreateMap<CreatePostRequest, Post>();
        CreateMap<UpdatePostRequest, Post>();

         CreateMap<CreateCommentRequest, Comment>();
         CreateMap<UpdateCommentRequest, Comment>();
   }
}
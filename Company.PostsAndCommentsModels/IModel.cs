namespace Company.PostsAndCommentsModels
{
    public interface IModel<in T>: IValidatable
    {
        int Id { get; set; }
        void Edit(T model);
    }
}

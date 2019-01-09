namespace PostsAndCommentsModels
{
    public interface IModel<in T>: IValidated
    {
        int Id { get; set; }
        void Edit(T model);
    }
}

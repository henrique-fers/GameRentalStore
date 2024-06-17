namespace GameRentalStore.Controllers.Models.Views
{
    public class ResultViewModel<T>
    {

        public ResultViewModel(string erro)
        {
            Errors.Add(erro);
            Sucess = false;
        }

        public ResultViewModel(List<string> erros)
        {
            Errors.AddRange(erros);
            Sucess = false;
        }
        public ResultViewModel(T data)
        {
            Data = data;
            Sucess = true;
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public bool Sucess { get; private set; }
    }
}
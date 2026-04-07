namespace Zoo.Src.Interfaces
{
    public interface IReproducible
    {
        bool CanBreedInCaptivity { get; }
        bool TryReproduce();
    }
}

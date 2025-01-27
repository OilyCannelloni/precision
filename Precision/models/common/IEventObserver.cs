namespace Precision.models.common;

public interface IEventObserver<in T>
{
    void OnNext(T @new);
}
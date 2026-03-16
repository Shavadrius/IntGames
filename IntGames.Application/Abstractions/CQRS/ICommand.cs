using IntGames.Domain.Abstractions;
using MediatR;

namespace IntGames.Application.Abstractions.CQRS;

public interface ICommand : IRequest<Result>, IBaseCommand { }

public interface ICommand<T>: IRequest<Result<T>>, IBaseCommand { }

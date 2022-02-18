namespace Domain.Util

type AsyncResult<'success, 'failure> = Async<Result<'success, 'failure>>

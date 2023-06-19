using STGenetics.Domain.Tools.ApiResponses;

namespace STGenetics.Domain.Tools
{
    public class Tools {
        public static ApiResponse<T> CreateResponse<T>(T data, Result result, int? DataCount)
        {
            return new ApiResponse<T>
            {
                Data = data,
                DataCount = DataCount,
                Result = result.GetDescription(),
            };
        }
    }
}

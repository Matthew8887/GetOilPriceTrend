using Exercise.Model;
using Microsoft.AspNetCore.Mvc;
using Exercise.Helper;
using Exercise.Service;
using System.Reflection;
using Exception = System.Exception;

namespace Exercise.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IOilService _service;

        public HomeController(IOilService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Status()
        {
            return new OkResult();
        }

        [HttpGet]
        public IActionResult Get(GeneralRequest? req)
        {
            try
            {
                if (req.IsNull() || req.Id.IsNull() || req.JsonRpc.IsNull() || req.Method.IsNullOrEmptyOrWhiteSpace() || req.Params.IsNull())
                    throw new ArgumentNullException("One or more mandatory params is/are null or empty");

                var magicType = Type.GetType(this.ToString(), throwOnError: true);
                var magicConstructor = magicType.GetConstructor(new[] { typeof(IOilService) });

                var magicClassObject = magicConstructor.Invoke(new object[] { new OilService() });
                var magicMethod = magicType.GetMethod(req.Method, BindingFlags.NonPublic | BindingFlags.Instance);
                if (magicMethod.IsNull())
                    throw new BadHttpRequestException($"Method '{req.Method}' not exists");

                try
                {
                    var magicValue = magicMethod.Invoke(magicClassObject, new object[] { req });
                    return new OkObjectResult(magicValue);
                }
                catch (TargetInvocationException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionResponse(ex.InnerException?.Message));
                }
            }
            catch (BadHttpRequestException ex)
            {
                return new BadRequestObjectResult(new ExceptionResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionResponse(ex.Message));
            }
        }

        internal GetOilPriceTrendResponse GetOilPriceTrend(GeneralRequest req)
        {
            var obj = req.Params.SerializeObject().DeserializeObject<GetOilPriceTrendRequest>();
            if (obj.IsNull() || obj.StartDateISO8601.IsNull() || obj.EndDateISO8601.IsNull())
                throw new ArgumentNullException("One or more mandatory params is/are null");

            if (obj.EndDateISO8601 < obj.StartDateISO8601)
                throw new ArgumentException("The end date cannot be earlier than the start date");

            var result = _service.GetByDate(obj.StartDateISO8601.Value, obj.EndDateISO8601.Value);

            return new GetOilPriceTrendResponse(req)
            {
                Result = new Result
                {
                    Prices = result.Select(x => new PriceObj(x.Date.GetValueOrDefault(), x.Price)).ToList()
                }
            };
        }
    }
}

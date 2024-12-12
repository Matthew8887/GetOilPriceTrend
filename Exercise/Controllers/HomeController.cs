using Exercise.Model;
using Microsoft.AspNetCore.Mvc;
using Exercise.Helper;
using Exercise.Service;
using System.Reflection;

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
                if (req.IsNull() || req.Id.IsNull() || req.JsonRpc.IsNull() || req.Method.IsNullOrEmptyOrWhiteSpace())
                    throw new ArgumentNullException("One or more mandatory params is/are null or empty");

                var magicType = Type.GetType(this.ToString(), throwOnError: true);
                var magicConstructor = magicType.GetConstructor(new[] { typeof(IOilService) });
                var magicClassObject = magicConstructor.Invoke(new object[] { new OilService() });
                var magicMethod = magicType.GetMethod(req.Method, BindingFlags.NonPublic | BindingFlags.Instance);
                if (magicMethod.IsNull())
                    throw new BadHttpRequestException($"Method {req.Method} not exists");

                var magicValue = magicMethod.Invoke(magicClassObject, new object[] { req });

                return new OkObjectResult(magicValue);
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

        internal GetOilPriceTrendResponse GetOilPriceTrend(GeneralRequest? req)
        {
            if (req.IsNull())
            {
                // we can choose if throws a new exception
            }

            if (req.Params.IsNull())
            {
                // we can choose if throws a new exception
            }

            var obj = req.Params.SerializeObject().DeserializeObject<GetOilPriceTrendRequest>();
            if (obj.IsNull() || obj.EndDateISO8601 < obj.StartDateISO8601)
            {
                // we can choose if throws a new exception
            }

            var result = _service.GetByDate(obj.StartDateISO8601, obj.EndDateISO8601);

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

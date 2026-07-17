using ERMS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly ApprovalService _approvalService;
        public ApprovalController(ApprovalService approvalService)
        {
            _approvalService = approvalService;
        }


    }
}

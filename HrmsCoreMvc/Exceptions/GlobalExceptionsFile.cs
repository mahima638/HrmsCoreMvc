namespace HrmsCoreMvc.Exceptions
{
    public class GlobalExceptionsFile
    {
        private readonly RequestDelegate req;
        private readonly ILogger<GlobalExceptionsFile> _logger;
        public GlobalExceptionsFile(RequestDelegate req, ILogger<GlobalExceptionsFile> _logger)
        {
            this.req = req;
            this._logger = _logger;
            
        }
        public async Task InvokeAsync(HttpContext context) {
            try
            {

                await req(context);

            }
            catch(Exception e) {

                _logger.LogError(e, "An Unhandled Exception Occurred");
                context.Response.Redirect("/Error/Index");
            }
        
        }

    }
}

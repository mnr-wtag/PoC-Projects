using Ninject.Extensions.ChildKernel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using RestApiNetDemo.BLL.IServices;
using RestApiNetDemo.BLL.Services;

namespace RestApiNetDemo.IoC
{
    public class NinjectResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectResolver() : this(new StandardKernel())
        {
        }

        public NinjectResolver(IKernel ninjectKernel, bool scope = false)
        {
            kernel = ninjectKernel;
            if (!scope)
            {
                AddBindings(kernel);
            }
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectResolver(AddRequestBindings(new ChildKernel(kernel)), true);
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void Dispose()
        {

        }

        private void AddBindings(IKernel kernel)
        {
            // singleton and transient bindings go here
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            kernel.Bind<IAdminService>().To<AdminService>().InSingletonScope();
            kernel.Bind<IAuthUserService>().To<AuthUserService>().InSingletonScope();
            kernel.Bind<ICourseService>().To<CourseService>().InSingletonScope();
            kernel.Bind<IDepartmentService>().To<DepartmentService>().InSingletonScope();
            kernel.Bind<IEnrollmentService>().To<EnrollmentService>().InSingletonScope();
            kernel.Bind<IStudentService>().To<StudentService>().InSingletonScope();
            kernel.Bind<ITeacherService>().To<TeacherService>().InSingletonScope();
            kernel.Bind<IUserProfileService>().To<UserProfileService>().InSingletonScope();
            
            return kernel;
        }
    }
}
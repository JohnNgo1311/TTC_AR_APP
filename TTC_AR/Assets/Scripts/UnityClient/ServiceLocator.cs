using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using ApplicationLayer.UseCases;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Net.Http;
using System.Reflection;

//! Không gắn vào GameObject: Không cần kế thừa MonoBehaviour hay gắn vào scene, 
//! Vì instance được quản lý tĩnh qua _instance.
public class ServiceLocator
{   //! Singleton
    private static ServiceLocator _instance;
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    //! Các Service cần được khởi tạo
    private readonly IGrapperService _grapperService;
    private readonly IRackService _rackService;
    private readonly IModuleService _moduleService;
    private readonly IJBService _jbService;
    private readonly IDeviceService _deviceService;
    private readonly IMccService _mccService;
    private readonly IFieldDeviceService _fieldDeviceService;
    private readonly IAdapterSpecificationService _adapterSpecificationService;
    private readonly IModuleSpecificationService _moduleSpecificationService;

    //! Khởi tạo Dependencies trong Constructor
    private ServiceLocator()
    {
        var httpClient = new HttpClient(); //? HttpClient là một service được sử dụng chung bởi các Repository

        IJBRepository IjbRepository = new JBRepository(httpClient);
        _jbService = new JBService(
            new JBUseCase(IjbRepository)
        );
        IDeviceRepository deviceRepository = new DeviceRepository(httpClient);
        _deviceService = new DeviceService(
            new DeviceUseCase(deviceRepository)
        );
        IAdapterSpecificationRepository adapterSpecificationRepository = new AdapterSpecificationRepository(httpClient);
        _adapterSpecificationService = new AdapterSpecificationService(
            new AdapterSpecificationUseCase(adapterSpecificationRepository)
        );
        ModuleSpecificationRepository moduleSpecificationRepository = new ModuleSpecificationRepository(httpClient);
        _moduleSpecificationService = new ModuleSpecificationService(
            new ModuleSpecificationUseCase(moduleSpecificationRepository)
        );
        // IRackRepository rackRepository = new RackRepository(httpClient);
        // _rackService = new RackService(
        //     new RackUseCase(rackRepository)
        // );

        // IGrapperRepository grapperRepository = new GrapperRepository(httpClient);
        // _grapperService = new GrapperService(
        //     new GrapperUseCase(grapperRepository)
        // );

        // IMccRepository mccRepository = new MccRepository(httpClient);
        // _mccService = new MccService(
        //     new MccUseCase(mccRepository)
        // );
    }

    //? Cung cấp các getter để các script truy cập service thông qua ServiceLocator.Instance.JBService.
    public IRackService RackService => _rackService;
    public IGrapperService GrapperService => _grapperService;
    public IModuleService ModuleService => _moduleService;
    public IJBService JBService => _jbService;
    public IDeviceService DeviceService => _deviceService;
    public IMccService MccService => _mccService;
    public IFieldDeviceService FieldDeviceService => _fieldDeviceService;
    public IAdapterSpecificationService AdapterSpecificationService => _adapterSpecificationService;
    public IModuleSpecificationService ModuleSpecificationService => _moduleSpecificationService;


}
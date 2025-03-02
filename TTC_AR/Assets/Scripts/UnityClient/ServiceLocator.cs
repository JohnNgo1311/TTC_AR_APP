using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using ApplicationLayer.UseCases;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System.Net.Http;
using UnityEngine.iOS;

public class ServiceLocator
{
    private static ServiceLocator _instance;
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();

    private readonly IGrapperService _grapperService;
    private readonly IRackService _rackService;
    private readonly IModuleService _moduleService;
    private readonly IJBService _jbService;
    private readonly IDeviceService _deviceService;
    private readonly IMccService _mccService;
    private readonly IFieldDeviceService _fieldDeviceService;
    private readonly IAdapterSpecificationService _adapterSpecificationService;
    private readonly IModuleSpecificationService _moduleSpecificationService;


    private ServiceLocator()
    {
        var httpClient = new HttpClient();
        IJBRepository jbRepository = new JBRepository(httpClient);
        _jbService = new JBService(
            new JBUseCase(jbRepository)
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
using Google.Protobuf;
using Grpc.Core;
using Leuze.RES.Gateways.OPC.UA.grpc;
using Leuze.RES.Services.Asset.grpc;

namespace GrpcTestOpcUaServer.Services;

public class AssetMock : AssetGrpc.AssetGrpcBase {

    private readonly ILogger<AssetMock> _logger;

    public AssetMock(ILogger<AssetMock> logger) {
        _logger = logger;
    }

    public override Task<KillTagResponse> KillTag(KillTagRequest request, ServerCallContext context) { 
        _logger.LogInformation("Received KillTag request for {id}", request.Identifier);

        return Task.FromResult(new KillTagResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<LockTagResponse> LockTag(LockTagRequest request, ServerCallContext context) {
        _logger.LogInformation("Received LockTag request for {id}", request.Identifier); 
        
        return Task.FromResult(new LockTagResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<ReadTagResponse> ReadTag(ReadTagRequest request, ServerCallContext context) {
        _logger.LogInformation("Received ReadTag request for {id}", request.Identifier);
        TagList.Instance.GetTags
            .TryGetValue(request.Identifier, out var value);

        return Task.FromResult(new ReadTagResponse {
            ResultCode = ResultCode.Good,
            ResultData = ByteString.CopyFromUtf8(value == null ? "NO VALUE!!" : value),
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<SetTagPasswordResponse> SetTagPassword(SetTagPasswordRequest request, ServerCallContext context) {
        _logger.LogInformation("Received SetTagPassword request for {id}", request.Identifier);

        return Task.FromResult(new SetTagPasswordResponse {
            Status = AutoIdOperationStatusEnumeration.Success0,
            ResultCode = ResultCode.Good
        });
    }

    public override Task<WriteTagResponse> WriteTag(WriteTagRequest request, ServerCallContext context) {
        _logger.LogInformation("Received WriteTag request for {id}", request.Identifier);

        TagList.Instance.GetTags[request.Identifier] = request.Data.ToStringUtf8();

        return Task.FromResult(new WriteTagResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<WriteTagIdResponse> WriteTagId(WriteTagIdRequest request, ServerCallContext context) {
        _logger.LogInformation("Received WriteTagId request for {id}", request.Identifier);

        var tags = TagList.Instance.GetTags;

        tags.TryGetValue(request.Identifier, out var value);

        if (value != null) {
            tags.Remove(request.Identifier);
            tags.Add(request.NewUid.ToStringUtf8(), value);    
        }

        return Task.FromResult(new WriteTagIdResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }
}

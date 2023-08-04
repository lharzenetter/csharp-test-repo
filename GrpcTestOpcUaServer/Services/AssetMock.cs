using Google.Protobuf;
using Grpc.Core;
using Leuze.RES.Services.Asset.grpc;
using System.Text;
using Leuze.RES.Gateways.OPC.UA.grpc;

namespace GrpcTestOpcUaServer.Services;

public class AssetMock : AssetGrpc.AssetGrpcBase {

    private readonly ILogger<AssetMock> _logger;
    private readonly Dictionary<string, string> tags = new Dictionary<string, string>();

    public AssetMock(ILogger<AssetMock> logger){
        _logger = logger;
    }

    public override Task<KillTagResponse> KillTag(KillTagRequest request, ServerCallContext context) {
        return base.KillTag(request, context);
    }

    public override Task<LockTagResponse> LockTag(LockTagRequest request, ServerCallContext context) {
        return base.LockTag(request, context);
    }

    public override Task<ReadTagResponse> ReadTag(ReadTagRequest request, ServerCallContext context) {
        tags.TryGetValue(request.Identifier, out var value);

        return Task.FromResult(new ReadTagResponse {
            ResultCode = ResultCode.Good,
            ResultData = ByteString.CopyFrom(value, Encoding.UTF8),
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<SetTagPasswordResponse> SetTagPassword(SetTagPasswordRequest request, ServerCallContext context) {
        return base.SetTagPassword(request, context);
    }

    public override Task<WriteTagResponse> WriteTag(WriteTagRequest request, ServerCallContext context) {
        tags[request.Identifier] = request.Data.ToStringUtf8();

        return Task.FromResult(new WriteTagResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<WriteTagIdResponse> WriteTagId(WriteTagIdRequest request, ServerCallContext context) {
        return base.WriteTagId(request, context);
    }
}

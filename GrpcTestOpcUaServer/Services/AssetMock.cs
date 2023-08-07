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
        return base.KillTag(request, context);
    }

    public override Task<LockTagResponse> LockTag(LockTagRequest request, ServerCallContext context) {
        return base.LockTag(request, context);
    }

    public override Task<ReadTagResponse> ReadTag(ReadTagRequest request, ServerCallContext context) {
        TagList.Instance.GetTags
            .TryGetValue(request.Identifier, out var value);

        return Task.FromResult(new ReadTagResponse {
            ResultCode = ResultCode.Good,
            ResultData = ByteString.CopyFromUtf8(value == null ? "NO VALUE!!" : value),
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<SetTagPasswordResponse> SetTagPassword(SetTagPasswordRequest request, ServerCallContext context) {
        return base.SetTagPassword(request, context);
    }

    public override Task<WriteTagResponse> WriteTag(WriteTagRequest request, ServerCallContext context) {
        TagList.Instance.GetTags[request.Identifier] = request.Data.ToStringUtf8();

        return Task.FromResult(new WriteTagResponse {
            ResultCode = ResultCode.Good,
            Status = AutoIdOperationStatusEnumeration.Success0
        });
    }

    public override Task<WriteTagIdResponse> WriteTagId(WriteTagIdRequest request, ServerCallContext context) {
        return base.WriteTagId(request, context);
    }
}

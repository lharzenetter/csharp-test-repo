pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0-preview'
        }
    }

    stages {
        stage ('Install dependencies') {
            steps {
                sh 'dotnet restore ./GrpcGreeter/GrpcGreeter.csproj'
                sh 'dotnet restore ./GrpcGreeterClient/GrpcGreeterClient.csproj'
            }
        }
        stage ('Build') {
            steps {
                sh 'dotnet build --configuration Release --no-restore ./GrpcGreeter/GrpcGreeter.csproj'
                sh 'dotnet build --configuration Release --no-restore ./GrpcGreeterClient/GrpcGreeterClient.csproj'
            }
        }
    }
}
pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:8.0-preview'
            // workaround to avoid jenkis to pass the jenkins user to the container
            args '-u root'
        }
    }

    stages {
        stage ('Install dependencies') {
            steps {
                sh 'dotnet restore GrpcGreeter/GrpcGreeter.csproj'
                sh 'dotnet restore GrpcGreeterClient/GrpcGreeterClient.csproj'
            }
        }
        stage ('Build') {
            parallel {
                stage ('Server') {
                    steps {
                        sh 'dotnet build --configuration Release --no-restore ./GrpcGreeter/GrpcGreeter.csproj'
                    }
                }
                stage ('Client') {
                    steps {
                        sh 'dotnet build --configuration Release --no-restore ./GrpcGreeterClient/GrpcGreeterClient.csproj'
                    }
                }
            
            }
        }
        stage ('Test') {
            stage ('GrpcGreeter.Tests') {
                steps {
                    sh 'dotnet test ./GrpcGreeter.Tests/GrpcGreeter.Tests.csproj'
                }
            }
        }
    }
}
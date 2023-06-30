pipeline {
    agent none

    stages {
        stage ('Build and Test') {
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
                                sh 'dotnet build -c Release ./GrpcGreeter/GrpcGreeter.csproj'
                            }
                        }
                        stage ('Client') {
                            steps {
                                sh 'dotnet build -c Release ./GrpcGreeterClient/GrpcGreeterClient.csproj'
                            }
                        }
                    
                    }
                }
                stage ('Test') {
                    steps {
                        sh 'dotnet test ./GrpcGreeter.Tests/GrpcGreeter.Tests.csproj'
                    }
                }
            }
        }
        stage ('Docker build') {
            agent {
                docker {
                    image 'docker:dind'
                    // -u root: workaround to avoid jenkis to pass the jenkins user to the container
                    // -v ... : workaround to enable docker to connect to the deamon
                    args '-u root -v /var/run/docker.sock:/var/run/docker.sock'
                }
                // label 'dockerDeamon'
            }
            stages {
                stage('Build Greeter Server') {
                    steps {
                        sh 'docker build -t grpcgreeter ./GrpcGreeter/'
                    }
                }
            }
        }
    }
}
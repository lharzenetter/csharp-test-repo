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
                    image 'docker:dind-rootless'
                    // workaround to avoid jenkis to pass the jenkins user to the container
                    // --privileged needed to build the images...
                    args '--privileged -u root'
                }
            }
            stages {
                stage('Build Greeter Server') {
                    steps {
                        sh 'cd ./GrpcGreeter/'
                        sh 'docker build'
                    }
                }
            }
        }
    }
}
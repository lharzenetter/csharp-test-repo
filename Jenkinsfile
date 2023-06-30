pipeline {
    agent none

    stages {
        stage ('Build and Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0-preview'
                    // workaround to avoid jenkis to pass the jenkins user to the container
                    args '-u root --pull always'
                }
            }
            stages {
                stage ('Install dependencies') {
                    steps {
                        sh '''
                            dotnet restore GrpcGreeter/GrpcGreeter.csproj
                            dotnet restore GrpcGreeterClient/GrpcGreeterClient.csproj
                        '''
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
                    // -v ... : workaround to enable docker to connect to the docker deamon
                    args '-u root --pull always -v /var/run/docker.sock:/var/run/docker.sock'
                }
            }
            environment {
                AZURE_CR_ACCESS_TOKEN = credentials('azure-cr-token')
            }
            stages {
                stage('Build and Push Container') {
                    steps {
                        // IMPORTANT: Use single quotes NOT double quotes! Otherwise the creds are printed to the console...
                        sh '''
                            docker login restesting.azurecr.io --username 00000000-0000-0000-0000-000000000000 --password-stdin <<< $AZURE_CR_ACCESS_TOKEN
                            docker build -t restesting.azurecr.io/res/grpcgreeter:latest ./GrpcGreeter/
                            docker push restesting.azurecr.io/res/grpcgreeter:latest
                            docker logout
                        '''
                    }
                }
                stage ('Clean-up') {
                    steps {
                        sh '''
                            docker system prune -a -f --volumes
                        '''
                    }
                }
            }
        }
    }
}
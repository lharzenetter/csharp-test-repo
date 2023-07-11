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
            environment {
                SONAR_TOKEN = credentials('test-project-sonar-token')
            }
            stages {
                stage('SonarQube Begin') {
                    steps {
                        sh '''
                            dotnet tool install --global dotnet-sonarscanner
                            export PATH="$PATH:/root/.dotnet/tools"
                            dotnet sonarscanner begin /k:"RFID_test_AYkw2FhmQSRf8kByoRWg" /d:sonar.host.url="https://res-dev.westeurope.cloudapp.azure.com/sonarqube" /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.login=$SONAR_TOKEN
                        '''
                    }
                }
                stage ('Build') {
                    steps {
                        sh 'dotnet build -c Release test.sln'
                    }
                }
                stage ('Test') {
                    steps {
                        sh '''
                            dotnet tool install --global dotnet-coverage
                            export PATH="$PATH:/root/.dotnet/tools"
                            dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'
                        '''
                    }
                }
                stage('SonarQube End') {
                    steps {
                        sh 'dotnet sonarscanner end /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml /d:sonar.login=$SONAR_TOKEN'
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
                            echo $AZURE_CR_ACCESS_TOKEN | docker login restesting.azurecr.io --username 00000000-0000-0000-0000-000000000000 --password-stdin
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
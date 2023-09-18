/* groovylint-disable CompileStatic */
pipeline {
    agent none

    stages {
        stage('Build and Test') {
            agent {
                docker {
                    image 'restesting.azurecr.io/dotnet-sonar/sdk:8.0-preview'
                    // workaround to avoid jenkis to pass the jenkins user to the container
                    args '-u root --pull always'
                }
            }
            environment {
                SONAR_TOKEN = credentials('test-project-sonar-token')
            }
            steps {
                withSonarQubeEnv(installationName: 'SonarQube') {
                    sh '''
                        git status
                        git branch -a

                        dotnet sonarscanner begin /k:"RFID_test_AYn-xYqTYbUcb1R9dfKP" \
                        /d:sonar.host.url="https://res-dev.westeurope.cloudapp.azure.com/sonarqube" \
                        /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml \
                        /d:sonar.scm.provider=git \
                        /d:sonar.token=sqp_c0ecc6280fef0a6fbb95bbdf129515f5921e239f

                        dotnet build -c Release test.sln
                        dotnet-coverage collect 'dotnet test' -f xml  -o 'coverage.xml'

                        dotnet sonarscanner end /d:sonar.token=sqp_c0ecc6280fef0a6fbb95bbdf129515f5921e239f

                        # ensure that everything has the rights of the outside jenkins user
                        chown -R 1001:999 .
                    '''

                    stash includes: '**/bin/Release/*/GrpcGreeter.dll', name: 'GRPCGreeter'
                    stash includes: '**/bin/Release/*/GrpcGreeterClient.dll', name: 'GRPCGreeterClient'
                }
            }
        }
        stage('Quality Gate') {
            steps {
                timeout(time: 2, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline:true
                }
            }
        }
        // stage('Docker from stash') {
        //     when {
        //         branch 'main'
        //     }
        //     agent {
        //         docker {
        //             image 'docker:dind'
        //             // -u root: workaround to avoid jenkis to pass the jenkins user to the container
        //             // -v ... : workaround to enable docker to connect to the docker deamon
        //             args '-u root --pull always -v /var/run/docker.sock:/var/run/docker.sock'
        //         }
        //     }
        //     steps {
        //         unstash 'GRPCGreeter'
        //         unstash 'GRPCGreeterClient'
        //         sh '''
        //             ls -al GrpcGreeter/bin/Release/net8.0
        //             ls -al GrpcGreeterClient/bin/Release/net8.0
        //         '''
        //     }
        // }
        // stage('Docker build') {
        //     when {
        //         branch 'main'
        //     }
        //     agent {
        //         label 'docker'
        //     }
        //     environment {
        //         AZURE_CR_ACCESS_TOKEN = credentials('azure-cr-token')
        //     }
        //     steps {
        //         // IMPORTANT: Use single quotes NOT double quotes! Otherwise the creds are printed to the console...
        //         sh '''
        //             echo $AZURE_CR_ACCESS_TOKEN | docker login restesting.azurecr.io --username 00000000-0000-0000-0000-000000000000 --password-stdin

        //             docker build -t restesting.azurecr.io/res/grpcgreeter:latest ./GrpcGreeter/
        //             docker push restesting.azurecr.io/res/grpcgreeter:latest

        //             docker logout
        //             docker system prune -a -f --volumes
        //         '''
        //     }
        // }
    }
}

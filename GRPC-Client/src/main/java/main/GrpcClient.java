package main;

import grpcClient.UserGrpc;
import grpcClient.UserOuterClass;
import io.grpc.ManagedChannel;
import io.grpc.ManagedChannelBuilder;

import java.util.Scanner;

public class GrpcClient {
    public static void main(String[] args) {
        ManagedChannel channel = ManagedChannelBuilder.forAddress("localhost", 5000).usePlaintext().build();
        UserGrpc.UserBlockingStub userStub = UserGrpc.newBlockingStub(channel);

        Scanner sc = new Scanner(System.in);
        while (true) {
            try {


                System.out.println("Choose one: 1: Register    2. Login");
                int choice = Integer.parseInt(sc.nextLine());
                System.out.print("Username: ");
                String userName = sc.nextLine();
                System.out.print("Password: ");
                String password = sc.nextLine();
                switch (choice) {
                    case 1: {
                        //        todo:register
                        UserOuterClass.UserModel userModel = UserOuterClass.UserModel.newBuilder()
                                .setUsername(userName)
                                .setPassword(password)
                                .build();
                        UserOuterClass.APIResponse response = userStub.createUser(userModel);
                        System.out.println(response);
                        break;
                    }
                    case 2: {
                        //todo: login
                        UserOuterClass.LoginRequest loginRequest = UserOuterClass.LoginRequest.newBuilder()
                                .setUsername(userName)
                                .setPassword(password)
                                .build();
                        UserOuterClass.APIResponse response = userStub.login(loginRequest);
                        System.out.println(response);
                        break;
                    }
                    default: {
                        return;
                    }
                }

            } catch (NumberFormatException e) {

            }
        }


//        System.out.println(response);
    }
}

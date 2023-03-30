# Remaster_Unity2D_BreakFriendShip
----------

프로젝트 소개
----------
1인 프로젝트로서, 기존 프로젝트 Unity2D_BreakFriendShip개선하고 새로운 스테이지를 추가했다. 또한 디자인적인 부분을 개선하였다.
개발 플랫폼은 PC Window이며 개발환경은 "Unity3d 2020.3.8f1"버전을 사용하였다.

## 프로젝트 추진 일정
![일정](https://user-images.githubusercontent.com/74814641/150344566-cbefc517-0022-48df-a623-9ab21bf12f35.JPG)

프로젝트 개발의 총 기간은 12일이다.

--------------
## 주요 기술
+ Unity2d
+ C#
+ Photon Cloud
+ GitHub

----------------
## 프로그램 동작도
![동작도](https://user-images.githubusercontent.com/74814641/150346827-42a542b7-58e4-4105-867a-784050d46da9.JPG)

----------------
## 조작법
+ ← 키보드 방향키 : 좌측이동 
+ → 키보드 방향키 : 우측이동
+ Space : 점프

--------------
## 기존 Break_Friendship과의 변경사항
### 1. 디자인적인 부분 변경(배경화면, UI등)

![디자인](https://user-images.githubusercontent.com/74814641/150348724-238319c7-ce49-4954-a2e9-e617d1fb860f.JPG)
![디자인2](https://user-images.githubusercontent.com/74814641/150348781-90732cd8-14d1-48d6-812f-b0db1f88ae92.JPG)

### 2. 강퇴기능 추가
![강퇴](https://user-images.githubusercontent.com/74814641/150349062-7e40c95f-be31-4e3d-8799-a9457d65c3fe.JPG)             
방장 플레이어만 x버튼, 왕관버튼이 보이며 캐릭터 오른쪽 상단에 위치한 x버튼을 누를시 해당 플레이어는 강퇴된다.


### 3. 방장주기기능 추가
![방장](https://user-images.githubusercontent.com/74814641/150349121-23331a82-89ed-4cdd-a8ec-542878f559bf.JPG)                 
방장 플레이어만 x버튼, 왕관버튼이 보이며 캐릭터 오른쪽 상단에 위치한 왕관버튼을 누를시 해당 플레이어는 방장이 된다.


### 4. 각 라운드별 힌트박스 추가
![힌트박스](https://user-images.githubusercontent.com/74814641/150349208-48df0476-b987-45f7-8170-4d462590f37c.JPG)                   
느낌표버튼을 누르게 되면 힌트가 적힌 텍스트창이 보이게 된다.


### 5. 네트워크 상태표시 화면 추가
![네트워크상태](https://user-images.githubusercontent.com/74814641/150354331-06139e0a-0e1a-4b53-aa89-ee8e0243bd26.png)                           
게임 플레이어들이 현재 네트워크 진행 상황을 알 수 있도록 네트워크 상태표시 화면 추가하였다.


### 6. 방에 접속한 클라이언트간 동기화문제 해결
시연영상을 보고 확인 할 수 있다.

--------------
## 프로그램 실행결과
+ 타이틀 화면
![타이틀](https://user-images.githubusercontent.com/74814641/150352353-5c3bc592-abc5-4c11-81f7-93579c3e08af.gif)
구름이 움직이는 듯한 동적인 화면이 연출된다.

+ 닉네임 화면
![닉네임](https://user-images.githubusercontent.com/74814641/150353097-5f2cbe3c-7a25-4644-bad7-0c3381ca2b16.gif)
닉네임을 입력하지 않고 버튼을 누르면 흔들리는 애니메이션 효과가 연출된다.

+ 캐릭터 선택 화면
![캐릭터 선택](https://user-images.githubusercontent.com/74814641/150353417-e4880e48-7a0b-45a5-9bfc-f27b34699b19.gif)                           
4가지 캐릭터를 선택할 수 있다.

+ 로비 화면
![로비](https://user-images.githubusercontent.com/74814641/150353918-a4a16e40-605d-4f1d-9888-46577c99475f.gif)
방 생성, 방 랜덤 입장 버튼이 있다. 또한 네트워크 진행 상황 화면이 출력된다.

+ 방 화면
![방](https://user-images.githubusercontent.com/74814641/150355123-70e43e2a-11fa-49fd-a6d8-afd41cbaf263.gif)
실시간 채팅, 방장 권한 아래 플레이어 강퇴 및 플레이어 방장 위임 기능이 있다.

+ Round1 화면
![라운드1_1](https://user-images.githubusercontent.com/74814641/150357215-7b6897a6-6b7a-4964-94e6-32b4f5e788de.gif)
튜토리얼이다.

+ Round2 화면
![라운드2](https://user-images.githubusercontent.com/74814641/150357736-bd4b0848-236a-4e29-812e-496d7ba339ae.gif)
단체로 총알을 맞지 않고 박스를 뿌셔 사과를 먹으면 된다.

+ Round3 화면
![라운드3](https://user-images.githubusercontent.com/74814641/150359895-4154daf4-b16e-42fd-92a7-192d59d9baa4.gif)
주어진 시간안에 3가지 장소에 랜덤으로 생성되는 사과를 먹으면 된다.

+ Round4 화면
![라운드4](https://user-images.githubusercontent.com/74814641/150359569-2286349a-95db-4cbd-972d-2b2b2313c85f.gif)
무궁화 꽃이 피었습니다.가 되면 움직이면 죽게 된다.

+ Round5 화면
![라운드5](https://user-images.githubusercontent.com/74814641/150360509-48f4bb04-8bea-48b8-b71f-c3a6fff9cf20.gif)
레이저를 맞지 않고 클리어 해야한다.

+ Round6 화면
![라운드6](https://user-images.githubusercontent.com/74814641/150360941-6cf8413d-34f0-4d67-8748-d3b5ff2b61f8.gif)
모든 플레이어가 앞을 보고 있으면 유령이 가장 짧은 거리에 있는 플레이어를 향해 간다. 한명이라도 뒤쪽을 보고있으면 멈춘다.

+ Round7 화면
![라운드7](https://user-images.githubusercontent.com/74814641/153880091-9cffaac7-e418-4cd2-8072-c4e4123f7cdd.gif)                   
빨간 불일때 플레이어 어느 한명이라도 움직이면 고스트가 처음 위치로 돌아간다. 각종 트랩을 닿아도 마찬가지로 처음 위치로 돌아간다.


+ 클리어 화면
![클리어 씬](https://user-images.githubusercontent.com/74814641/150361426-d5b18bd8-0438-4c6e-8507-817b146b7652.JPG)

--------------
## 버전 기록

+ ### V 1.0.0
1. 라운드3에서 죽으면 타이머 보이지 않음 
=> 타이머 오브젝트 활성화로 해결

2. 타이머 동기화 살짝 안됨
=> Photon함수 AllViaServer을 통해 해결

3. 가끔 죽으면 ArrayBoundaryError 발생
=> 방장 강퇴시 PV.OwnerNumber가 상승되어 에러 생김, PV.OwnerNumber의 오름차순 정렬 함수를 만들어 해결

4. 라운드5에서 레이저 닿았을 때 본인은 죽었으나 다른 클라이언트 플레이어 안죽음
=> 방패크기 0.75에서 1.25로 조절


+ ### V 1.0.1
1. 방장만 ESC버튼을 눌렀을 때 Regame할 수 있는 화면 추가

+ ### V 1.0.2
1. 리스폰 했을 때 캐릭터 가끔 생성되지 않음
=> 죽었을 시 현재 씬을 ReLoad하는 것이 아니고 스폰지역으로 되돌아 가도록 하여 해결

2. 클라이언트마다 고스트 속도가 다르는 버그
=> 클라이언트마다 컴퓨터 사양이 다르기 때문에 Update문 대신 FixedUpdate로 해결

3. 라운드6 톱니 동기화가 되지 않음
=> Time.time에서 Time.DeltaTime으로 변경 버그 해결

+ ### V 1.0.3
1. 한꺼번에 여러캐릭터 죽었을 때 죽는 화면 여러개 뜨는 버그
=> 코드 변경으로 해결

2. 리스폰시 사과 실루엣 초기화 안되는 버그
=> 코드 변경으로 해결

3. 라운드 3 사과 위치 랜덤 초기화 안되는 버그
=> 코드 변경으로 해결

4. 라운드 5 리스폰 도중 레이저 맞는 버그
=> Invoke함수로 update안 if문 제어하는 변수를 1.5초 뒤에 초기화

5. 라운드 4 사과 비활성화 버그
=> Destroy 대신 SetActive(False)를 통해 해결

+ ### V 1.0.4
1. 라운드5 엘리베이터 위에 죽을 시 엘리베이터 위치 초기화 안되는 버그
=> 죽는 동안 실행되지 않도록 코드 변경 해결

+ ### V 1.0.5
1. 톱니 버그
=> IsTrigger 체크 

### V.1.0.6
1. 라운드 7 추가




--------------
## 업데이트 안내
1. 라운드7 추가 (2022.02.14)

라운드7 시연 영상
+ https://youtu.be/ovGQKIvlOJI

--------------
## 시연연상
+ https://youtu.be/546uUgdAwZw



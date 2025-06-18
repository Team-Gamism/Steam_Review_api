# Steam_Review_api
Steam 리뷰 데이터를 기반으로 한 API 서버입니다.

Steam 리뷰 CSV 파일을 전처리하고, MySQL DB에 저장한 후, 이를 API로 제공합니다.

<br>

> 데이터셋 링크: https://www.kaggle.com/datasets/arashnic/game-review-dataset

<br>

## 사용 기술
- C#
- ASP.NET Core
- MySQL
- Python
- Cloudtype
- Github Action

<br>
  

## 프로젝트 설명
2025년 1학기 빅데이터 프로젝트.

Steam 리뷰 csv 데이터를 전처리 후 DB에 저장.

해당 데이터 관련 API 제공


## game_review_data 테이블
```sql
create table game_review_data(
    review_id int not null,
    game varchar(100) not null,
    year INT NOT NULL,
    review TEXT NOT NULL,
    sentiment VARCHAR(20) NOT NULL,
    language VARCHAR(10) NOT NULL,
    INDEX idx_game_year (game, year),
    INDEX idx_sentiment (sentiment),
    INDEX idx_language (language)
)
```

<br>

## 배포
배포 주소 : https://port-0-steam-review-api-mc0mbx5fdff774bd.sel5.cloudtype.app
> Cloudtype 사용 배포

<br>


## API

| Method | Endpoint                                | 설명                     |
|--------|------------------------------------------|--------------------------|
| GET    | `/api/review/{id}`                      | 리뷰 ID를 통한 리뷰 조회 |
| GET    | `/api/review/games`    | 모든 리뷰가 적힌 게임 이름 조회 |
| GET    | `/api/review/average?game={game}` | 게임 이름을 통한 평균 평점 조회 |
| POST   | `/api/review` | 리뷰 추가 |

---

> ⚠️ 주의:
> 현재 서버는 Cloudtype의 무료 플랜으로 배포되어 있어, 일정 시간 동안 사용이 없으면 자동으로 종료될 수 있습니다.
> 
> 이 경우, 처음 접속 시 응답이 지연될 수 있으니 잠시 기다려 주세요.

<br>

## 기여 방법
이 프로젝트에 기여하고 싶으시다면, 아래 절차를 따라주세요:

1. 이 저장소를 포크(Fork) 합니다.
2. 로컬에서 git clone 합니다.
3. 새로운 브랜치를 생성합니다. 예: `feature/새기능`
4. 원하는 기능을 추가하거나 버그를 수정합니다.
5. 커밋 메시지를 명확하게 작성합니다. 예: `feat: 리뷰 필터 기능 추가`
6. 본인의 GitHub 저장소로 푸시합니다.
7. Pull Request(PR) 를 생성합니다:

<br>

>PR은 코드 리뷰 후 병합됩니다.
>
>가능한 한 작은 단위로 커밋하고, 기능별로 PR을 나눠주세요.

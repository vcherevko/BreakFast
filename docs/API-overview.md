# Authorization REST API

## POST /register

**Query:**

*Body:*
```
{
    email,
    phone,
    name,
    role
}
```

**Response:**

*Possible HTTP statuses:*
```
200, {id}
400
500
```

```
{
    error_message: $message
}
```

## POST /login

**Query:**

*Body:*
```
{
    login,
    password
}
```

**Response:**

*Possible HTTP statuses:*
```
200, {token, expiration}
400
500
```
```
{
    error_message: $message
}
```

# Customer REST API

## GET /orders

**Query:**

*Filters:*

`?status=active/completed/canceled`

*Headers:*

`x-auth-token: $token`

*Body:*
```
{
    user_id: {id},
    page_index,
    page_count
}
```

**Response:**

*Possible HTTP statuses:*
```
200
400
401(access denied)
500
```

```
{
    page_index,
    page_count,
    orders: [
        {
            id,
            status,
            restaurant: {
                name
            },
            timestamp,
            items: [
                {
                    price,
                    quantity,
                    description,
                    image_url
                }
            ]
        }
    ]
}
```


## GET /order/{id}

**Query:**

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    user_id: {id}
}
```

**Response:**

*Possible HTTP statuses:*
```
200
400
401(access denied)
403(forbidden)
404
500
```
```
{
    id,
    status,
    restaurant: {
        name
    },
    timestamp,
    items: [
        {
            price,
            quantity,
            description,
            image_url
        }
    ]
}
```

## POST /order

**Query:**

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    user_id: {id},
    restaurant_id,
    menu_items: [
        {
            menu_item_id,
            quantity
         }
    ]
}
```

**Response:**

*Possible HTTP statuses:*
```
201 (created)
400
500
```

```
{
    id,
    secret_payment_url,
    estimated_time_of_arrival
}
```

# Restaurant REST API

## GET /orders

**Query:**

*Filters:*

`?status=active/completed/canceled`

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    restaurant_id: {id},
    page_index,
    page_count
}
```

**Response:**

*Possible HTTP statuses:*
```
200
400
401(access denied)
500
```

```
{
    page_index,
    page_count,
    orders: [
        {
            id,
            status,
            timestamp,
            menu_items: [
                {
                    quantity,
                    menu_item_id
                }
            ]
        }
    ]
}
```

## GET /order/{id}

**Query:**

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    restaurant_id: {id}
}
```

**Response:**

*Possible HTTP statuses:*

```
200
400
401(access denied)
403(forbidden)
404
500
```

```
{
    id,
    status,
    timestamp,
    menu_items: [
        {
            quantity,
            menu_item_id
        }
    ]
}
```

## PUT/PATCH /order/{id}

**Query:**

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    restaurant_id,
    action: “deny/complete/accept”
}
```

**Response:**

*Possible HTTP statuses:*

```
200
400
401
403
404
500
```

# Courier REST API

## GET /deliveries

**Query:**

*Filters:*

`?status=active/available/completed/canceled`

*Header:*

`x-auth-token: $token`

*Body:*

```
{
courier_id: {id},
page_index,
page_count
}
```

**Response:**

*Possible HTTP statuses:*
```
200
400
401(access denied)
500
```

```
{
    page_index,
    page_count,
    deliveries: [
        {
            id,
            status,
            order_id,
            restaurant: {
                name,
                address,
                phone,
                distance
            },
            customer: {
                address,
                phone,
                distance
            },
            reward
        }
    ]
}
```

## PUT/PATCH /delivery/{id}

**Query:**

*Header:*

`x-auth-token: $token`

*Body:*

```
{
    courier_id,
    action: “complete/accept”
}
```

**Response:**

*Possible HTTP statuses:*
```
200
400
401
403
404
500
```

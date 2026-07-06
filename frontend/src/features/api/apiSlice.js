import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { selectToken } from '../auth/authSlice'

const baseQuery = fetchBaseQuery({
  baseUrl: '/api',
  prepareHeaders: (headers, { getState }) => {
    const token = selectToken(getState())
    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }
    return headers
  },
})

export const api = createApi({
  reducerPath: 'api',
  tagTypes: ['Guests', 'Todos'],
  baseQuery: baseQuery,
  endpoints: (builder) => ({
    signup: builder.mutation({
      query: (credentials) => ({
        url: '/auth/signup',
        method: 'POST',
        body: credentials,
      }),
    }),
    login: builder.mutation({
      query: (credentials) => ({
        url: '/auth/login',
        method: 'POST',
        body: credentials,
      }),
    }),
    getGuests: builder.query({
      query: () => '/guests',
      providesTags: ['Guests'],
    }),
    addGuest: builder.mutation({
      query: (guest) => ({
        url: '/guests',
        method: 'POST',
        body: guest,
      }),
      invalidatesTags: ['Guests'],
    }),
    getTodos: builder.query({
      query: () => '/todo/todos',
      providesTags: ['Todos'],
    }),
    createTodo: builder.mutation({
      query: (todo) => ({
        url: '/todo/todos',
        method: 'POST',
        body: todo,
      }),
      invalidatesTags: ['Todos'],
    }),
    deleteTodo: builder.mutation({
      query: (id) => ({
        url: `/todo/todos/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Todos'],
    }),
  }),
})

export const {
  useSignupMutation,
  useLoginMutation,
  useGetGuestsQuery,
  useAddGuestMutation,
  useGetTodosQuery,
  useCreateTodoMutation,
  useDeleteTodoMutation,
} = api
export default api

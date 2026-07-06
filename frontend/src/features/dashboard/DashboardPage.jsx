import { useState } from 'react'
import { useSelector } from 'react-redux'
import { selectToken } from '../auth/authSlice'
import { useGetEventsQuery, useCreateEventMutation, useGetGuestsQuery } from '../api/apiSlice'
import PageShell from '../../components/PageShell'

function CreateEventForm({ onCreateEvent, isCreatingEvent }) {
  const [name, setName] = useState('')
  const [date, setDate] = useState('')
  const [location, setLocation] = useState('')
  const [description, setDescription] = useState('')
  const [error, setError] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault()
    setError('')
    if (!name.trim() || !date || !location.trim()) {
      setError('Please fill in all required fields.')
      return
    }
    await onCreateEvent({
      name: name.trim(),
      date: new Date(date).toISOString(),
      location: location.trim(),
      description: description.trim(),
    })
    setName('')
    setDate('')
    setLocation('')
    setDescription('')
  }

  return (
    <form className="create-event-form" onSubmit={handleSubmit}>
      <div className="create-event-form__header">
        <span className="create-event-form__icon">💍</span>
        <h2>Create your first event</h2>
        <p>Set up the wedding event details to get started.</p>
      </div>

      <div className="create-event-form__grid">
        <label className="field">
          <span className="field__label">Event name</span>
          <input
            className="field__input"
            placeholder="e.g. Emily & James Wedding"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </label>

        <label className="field">
          <span className="field__label">Date</span>
          <input
            className="field__input"
            type="date"
            value={date}
            onChange={(e) => setDate(e.target.value)}
            required
          />
        </label>

        <label className="field">
          <span className="field__label">Location</span>
          <input
            className="field__input"
            placeholder="e.g. Grand Hyatt, Mumbai"
            value={location}
            onChange={(e) => setLocation(e.target.value)}
            required
          />
        </label>
      </div>

      <label className="field">
        <span className="field__label">Description (optional)</span>
        <textarea
          className="field__input field__textarea"
          placeholder="A few words about your special day..."
          rows={3}
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </label>

      {error ? <p className="field__error" role="alert">{error}</p> : null}

      <button type="submit" className="create-event-form__submit" disabled={isCreatingEvent}>
        {isCreatingEvent ? 'Creating...' : 'Create Event'}
      </button>
    </form>
  )
}

function EventHeader({ event }) {
  const formattedDate = event?.date
    ? new Date(event.date).toLocaleDateString('en-IN', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      })
    : 'Date TBD'

  return (
    <div className="event-details">
      <p className="eyebrow">Your wedding</p>
      <h1>{event?.name}</h1>
      <div className="event-meta">
        <span className="event-meta__item">📅 {formattedDate}</span>
        {event?.location && (
          <span className="event-meta__item">📍 {event.location}</span>
        )}
      </div>
      {event?.description && (
        <p className="event-description">{event.description}</p>
      )}
    </div>
  )
}

export default function DashboardPage() {
  const token = useSelector(selectToken)
  const { data: guests = [], isLoading: guestsLoading, isError: guestsError } = useGetGuestsQuery(undefined, {
    skip: !token,
  })
  const { data: events = [], isLoading: eventsLoading, isError: eventsError } = useGetEventsQuery(undefined, {
    skip: !token,
  })
  const [createEvent, { isLoading: isCreatingEvent }] = useCreateEventMutation()

  const currentEvent = events[0]
  const hasEvents = events.length > 0

  if (!hasEvents) {
    return (
      <PageShell>
        <section className="dashboard-card">
          <div className="empty-state">
            {eventsLoading ? (
              <div className="empty-state__inner">
                <p className="status-pill">Loading events...</p>
              </div>
            ) : eventsError ? (
              <div className="empty-state__inner">
                <span className="create-event-form__icon">⚠️</span>
                <h2>Unable to load events</h2>
                <p className="status-pill status-pill--error">Please try again later.</p>
              </div>
            ) : (
              <CreateEventForm
                onCreateEvent={createEvent}
                isCreatingEvent={isCreatingEvent}
              />
            )}
          </div>
        </section>
      </PageShell>
    )
  }

  return (
    <PageShell>
      <section className="dashboard-card">
        <EventHeader event={currentEvent} />

        {guestsLoading ? (
          <p className="status-pill">Loading guests...</p>
        ) : guestsError ? (
          <p className="status-pill status-pill--error">Unable to load guest data.</p>
        ) : (
          <div className="dashboard-section">
            <h3 className="dashboard-section__title">
              Guest List
              <span className="dashboard-section__count">{guests.length}</span>
            </h3>
            {guests.length > 0 ? (
              <ul className="guest-list">
                {guests.map((guest) => (
                  <li key={guest.id ?? guest.name} className="guest-list__item">
                    <strong>{guest.name}</strong>
                    <span>{guest.email ?? 'No email provided'}</span>
                  </li>
                ))}
              </ul>
            ) : (
              <p className="dashboard-section__empty">No guests yet. Add guests from the API to see them here.</p>
            )}
          </div>
        )}
      </section>
    </PageShell>
  )
}

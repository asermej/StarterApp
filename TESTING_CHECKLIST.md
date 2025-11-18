# Testing Checklist for Authentication & Error Handling

## Pre-Testing Setup

- [ ] Ensure the backend API is running (`dotnet run` from `apps/api/Platform.Api`)
- [ ] Ensure the frontend is running (`npm run dev` from `apps/web/Platform.Web`)
- [ ] Clear browser cookies and local storage for a clean test
- [ ] Have two browser windows ready (one logged in, one logged out)

## 1. Authorization Testing

### Public Endpoints (Should work WITHOUT authentication)

#### Topics
- [ ] GET `/api/v1/Topic/{id}` - View individual topic
- [ ] GET `/api/v1/Topic?pageNumber=1&pageSize=10` - Search topics
- [ ] GET `/api/v1/Topic/feed` - View topic feed
- [ ] GET `/api/v1/Topic/{id}/tags` - View topic tags

#### Personas
- [ ] GET `/api/v1/Persona/{id}` - View individual persona
- [ ] GET `/api/v1/Persona?pageNumber=1&pageSize=10` - Search personas
- [ ] GET `/api/v1/Persona/{id}/training` - View training data
- [ ] GET `/api/v1/Persona/{id}/categories` - View persona categories

#### Categories
- [ ] GET `/api/v1/Category/{id}` - View individual category
- [ ] GET `/api/v1/Category?pageNumber=1&pageSize=10` - Search categories

#### Tags
- [ ] GET `/api/v1/Tag/{id}` - View individual tag
- [ ] GET `/api/v1/Tag?pageNumber=1&pageSize=10` - Search tags

### Protected Endpoints (Should return 401 WITHOUT authentication)

#### Topics
- [ ] POST `/api/v1/Topic` - Create topic (should return 401)
- [ ] PUT `/api/v1/Topic/{id}` - Update topic (should return 401)
- [ ] DELETE `/api/v1/Topic/{id}` - Delete topic (should return 401)

#### Personas
- [ ] POST `/api/v1/Persona` - Create persona (should return 401)
- [ ] PUT `/api/v1/Persona/{id}` - Update persona (should return 401)
- [ ] DELETE `/api/v1/Persona/{id}` - Delete persona (should return 401)

#### Chats
- [ ] POST `/api/v1/Chat` - Create chat (should return 401)
- [ ] GET `/api/v1/Chat?personaId={id}` - List chats (should return 401)
- [ ] PUT `/api/v1/Chat/{id}` - Update chat (should return 401)
- [ ] DELETE `/api/v1/Chat/{id}` - Delete chat (should return 401)

#### Messages
- [ ] POST `/api/v1/Message/send` - Send message (should return 401)
- [ ] GET `/api/v1/Message?chatId={id}` - List messages (should return 401)

#### Images
- [ ] POST `/api/v1/Image/upload` - Upload image (should return 401)

## 2. Session Timeout Testing

### Scenario 1: Active User
1. [ ] Log in to the application
2. [ ] Wait 10 minutes while occasionally clicking around
3. [ ] Perform an action (e.g., create a topic)
4. [ ] **Expected:** Action succeeds (session extended)

### Scenario 2: Inactive User
1. [ ] Log in to the application
2. [ ] Wait 25 hours without any interaction (or adjust Auth0 settings for faster test)
3. [ ] Try to perform an action
4. [ ] **Expected:** Automatic redirect to login page

### Scenario 3: Token Refresh
1. [ ] Log in to the application
2. [ ] Open browser dev tools > Application > Cookies
3. [ ] Note the Auth0 session cookie
4. [ ] Wait 5 minutes and perform an action
5. [ ] Check if the cookie timestamp updated
6. [ ] **Expected:** Cookie refreshed on activity

## 3. Error Handling Testing

### Business Exceptions (User-Facing Errors)

#### Duplicate Email Test
1. [ ] Create a persona with email "test@example.com"
2. [ ] Try to create another persona with the same email
3. [ ] **Expected:** Toast shows "Email already exists" or similar
4. [ ] **Expected:** Error message is specific and actionable

#### Validation Errors Test
1. [ ] Try to create a topic without required fields
2. [ ] **Expected:** Toast shows specific validation message
3. [ ] **Expected:** User can understand what to fix

#### File Upload Errors Test
1. [ ] Try to upload a file larger than 5MB
2. [ ] **Expected:** Toast shows "File size must be under 5MB"
3. [ ] Try to upload a non-image file
4. [ ] **Expected:** Toast shows "Please upload an image file"

### Technical Exceptions (Generic Error Messages)

#### Simulated Database Error
1. [ ] Stop the database or API server
2. [ ] Try to perform any action
3. [ ] **Expected:** Toast shows "Something went wrong" (not internal error details)
4. [ ] **Expected:** Full error logged to browser console

#### Network Error Test
1. [ ] Disconnect from internet
2. [ ] Try to perform an action
3. [ ] **Expected:** Toast shows "Connection error"
4. [ ] Reconnect to internet
5. [ ] Try the action again
6. [ ] **Expected:** Action succeeds

## 4. UI/UX Testing

### Success Messages
- [ ] Create a persona
- [ ] **Expected:** Green toast: "Persona created successfully!"
- [ ] **Expected:** Redirect to personas list

- [ ] Update a persona
- [ ] **Expected:** Green toast: "Persona updated successfully!"

- [ ] Delete a persona
- [ ] **Expected:** Green toast: "Persona deleted successfully!"

### Loading States
- [ ] Click a button that performs a server action
- [ ] **Expected:** Button shows loading spinner
- [ ] **Expected:** Button is disabled during loading
- [ ] **Expected:** Loading state clears after completion

### Error States
- [ ] Trigger a validation error
- [ ] **Expected:** Error message appears below form
- [ ] **Expected:** Toast notification appears
- [ ] Fix the error and resubmit
- [ ] **Expected:** Error message clears

## 5. Image Upload Testing

### Authenticated Upload
1. [ ] Log in to the application
2. [ ] Navigate to create persona page
3. [ ] Upload an image
4. [ ] **Expected:** Image uploads successfully
5. [ ] **Expected:** Preview appears
6. [ ] **Expected:** Image saved with persona

### Unauthenticated Upload
1. [ ] Log out
2. [ ] Try to upload an image (if possible from public page)
3. [ ] **Expected:** 401 error or redirect to login

### Large File Compression
1. [ ] Upload an image larger than 5MB
2. [ ] **Expected:** Automatic compression
3. [ ] **Expected:** Progress bar shows compression
4. [ ] **Expected:** Upload succeeds with compressed file

## 6. Edge Cases

### Session Expired During Form Fill
1. [ ] Log in
2. [ ] Start filling out a long form (don't submit yet)
3. [ ] Wait for session to expire (or manually delete Auth0 cookies)
4. [ ] Try to submit the form
5. [ ] **Expected:** Redirect to login (form data may be lost - acceptable)

### Multiple Errors
1. [ ] Fill form with multiple validation errors
2. [ ] Submit
3. [ ] **Expected:** First error shown (or all errors if implemented)

### Rapid Submissions
1. [ ] Double-click submit button rapidly
2. [ ] **Expected:** Only one request sent
3. [ ] **Expected:** Button disabled after first click

## 7. Browser Console Checks

### No Console Errors
- [ ] Navigate through the app
- [ ] **Expected:** No unhandled errors in console
- [ ] **Expected:** Only expected warnings (if any)

### Error Details Logged
- [ ] Trigger a technical exception
- [ ] Check browser console
- [ ] **Expected:** Full error details with stack trace
- [ ] **Expected:** Error type clearly indicated

### Network Requests
- [ ] Open Network tab in dev tools
- [ ] Perform various actions
- [ ] **Expected:** Authorization headers present on protected requests
- [ ] **Expected:** 401 responses trigger login redirect
- [ ] **Expected:** Error responses have structured JSON body

## 8. Cross-Browser Testing

Test the above scenarios in:
- [ ] Chrome/Chromium
- [ ] Firefox
- [ ] Safari (macOS)
- [ ] Edge

## 9. Performance Testing

### API Response Times
- [ ] Create 10 personas in succession
- [ ] **Expected:** Each request completes in < 2 seconds
- [ ] **Expected:** No memory leaks or performance degradation

### Error Handler Performance
- [ ] Trigger 5 errors rapidly
- [ ] **Expected:** Each toast displays correctly
- [ ] **Expected:** No UI freezing or lag

## 10. Accessibility Testing

### Keyboard Navigation
- [ ] Navigate forms using only keyboard
- [ ] **Expected:** All interactive elements accessible
- [ ] **Expected:** Error messages announced by screen readers

### Screen Reader Testing
- [ ] Use screen reader (VoiceOver on Mac, NVDA on Windows)
- [ ] Trigger an error
- [ ] **Expected:** Error message read aloud
- [ ] Submit successfully
- [ ] **Expected:** Success message read aloud

## Test Results

### Test Summary
- Total Tests: ___
- Passed: ___
- Failed: ___
- Blocked: ___

### Critical Issues Found
1. 
2. 
3. 

### Minor Issues Found
1. 
2. 
3. 

### Notes
- 
- 
- 

## Sign-Off

- [ ] All critical tests passed
- [ ] All critical issues resolved
- [ ] Documentation updated
- [ ] Ready for production

**Tested by:** ___________________  
**Date:** ___________________  
**Environment:** ___________________

